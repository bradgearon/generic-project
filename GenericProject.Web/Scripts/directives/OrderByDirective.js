'use strict';

// TODO: A better directive name than 'gen-order-by'
angular.module('GenericApp')
  .directive('genOrderBy', function () {
      return function (scope, elm, attrs) {
          elm.addClass('btn  btn-default');
          var $icon = $('<i class="glyphicon">');
          elm.append($icon);

          elm.on('click', function () {
              var $elm = $(elm);
              $elm.siblings()
                  .removeClass('ordered')
                  .removeClass('ordered-desc');

              if ($elm.hasClass('ordered-desc')) {
                  $elm.removeClass('ordered-desc');
              }
              else if ($elm.hasClass('ordered')) {
                  $elm.addClass('ordered-desc');
              }

              $elm.addClass('ordered');
          });
      };
  });