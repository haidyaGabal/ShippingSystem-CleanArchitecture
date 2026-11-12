const ApiClient = {
    baseUrl: 'https://localhost:7058',

    get: function (url, onSuccess, onError, useAuth = true, retried = false) {
        const accessToken = AppHelper.getCookie("AccessToken");
        const headers = useAuth && accessToken
            ? { 'Authorization': 'Bearer ' + accessToken }
            : {};

        $.ajax({
            url: this.baseUrl + url,
            type: 'GET',
            contentType: 'application/json',
            headers: headers,
            xhrFields: {
                withCredentials: true
            },
            success: onSuccess,
            error: function (xhr) {
                if (useAuth && xhr.status === 401 && !retried) {
                    ApiClient.refreshToken(() => {
                        ApiClient.get(url, onSuccess, onError, useAuth, true);
                    }, onError);
                } else if (onError) {
                    onError(xhr);
                }
            }
        });
    },

    post: function (url, data, onSuccess, onError, useAuth = true, retried = false) {
        const accessToken = AppHelper.getCookie("AccessToken");
        const headers = useAuth && accessToken
            ? { 'Authorization': 'Bearer ' + accessToken }
            : {};

        $.ajax({
            url: this.baseUrl + url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers,
            xhrFields: {
                withCredentials: true
            },
            success: onSuccess,
            error: function (xhr) {
                if (useAuth && xhr.status === 401 && !retried) {
                    ApiClient.refreshToken(() => {
                        ApiClient.post(url, data, onSuccess, onError, useAuth, true);
                    }, onError);
                } else if (onError) {
                    onError(xhr);
                }
            }
        });
    },

    delete: function (url, onSuccess, onError, useAuth = true, retried = false) {
        const accessToken = AppHelper.getCookie("AccessToken");
        const headers = useAuth && accessToken
            ? { 'Authorization': 'Bearer ' + accessToken }
            : {};

        $.ajax({
            url: this.baseUrl + url,
            type: 'DELETE',
            headers: headers,
            xhrFields: {
                withCredentials: true
            },
            success: onSuccess,
            error: function (xhr) {
                if (useAuth && xhr.status === 401 && !retried) {
                    ApiClient.refreshToken(() => {
                        ApiClient.delete(url, onSuccess, onError, useAuth, true);
                    }, onError);
                } else if (onError) {
                    onError(xhr);
                }
            }
        });
    },

    refreshToken: function (onSuccess, onFailure) {
        $.ajax({
            url: this.baseUrl + '/api/auth/RefreshAccessToken',
            type: 'POST',
            contentType: 'application/json',
            xhrFields: {
                withCredentials: true
            },
            success: function (response) {
                const token = response && (response.AccessToken || response.accessToken);
                if (token) {
                    document.cookie = `AccessToken=${token}; path=/`;
                    onSuccess();
                } else if (onFailure) {
                    onFailure({ message: 'Token refresh failed.' });
                }
            },
            error: function (err) {
                if (onFailure) onFailure(err);
            }
        });
    }
};