app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/Scripts/templates/FlightList.cshtml',
            controller: 'LandingPageController'
        }).
        when('/AddNewFlight/:gateid', {
            templateUrl: '/Scripts/templates/AddFlight.cshtml',
            controller: 'AddFlightController'
        }).
        when('/UpdateFlight/:gateid/:flightno', {
            templateUrl: '/Scripts/templates/UpdateFlight.cshtml',
            controller: 'UpdateFlightController'
        }).
        when('/CancelFlight/:gateid/:flightno', {
               templateUrl: '/Scripts/templates/UpdateFlight.cshtml',
               controller: 'CancelFlightController'
           }).
        otherwise({
            redirectTo: '/'
        });
  }]);