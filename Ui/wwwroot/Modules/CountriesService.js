const CountriesService = {
    GetAll: function (onSuccess, onError) {
        ApiClient.get('/api/Countrys', onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/Countrys/${id}`, onSuccess, onError, false);
    }
};