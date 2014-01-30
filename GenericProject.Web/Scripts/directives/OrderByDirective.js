'use strict';

// TODO: A better directive name than 'gen-order-by'
angular.module('GenericApp')
  .directive('genOrderBy', function () {
      return function (scope, elm, attrs) {
          elm.on('click', function () {
              var $elm = $(elm);
              $elm.siblings().not(elm).removeClass('ordered');
              $elm.siblings().not(elm).removeClass('ordered-desc');
              if ($elm.hasClass('ordered')) $elm.addClass('ordered-desc'); else $elm.removeClass('ordered-desc');
              $elm.addClass('ordered');
          });
      };
  });