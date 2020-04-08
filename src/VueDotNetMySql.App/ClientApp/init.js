import './sass/app.scss'
import 'core-js/es6/promise'
import 'core-js/es6/array'

// window._ = require('lodash');
// window.Popper = require('popper.js').default;

//window.ajax = require('ajax');
//window.DataTable = require('datatables.net');

try {
  window.$ = window.jQuery = require('jquery');

  // require('bootstrap');
} catch (e) {

  console.log('Failed Bootstrap Failed To Render.', e);
}

window.axios = require('axios');

window.axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

// let token = document.head.querySelector('meta[name="csrf-token"]');

// if (token) {
//     window.axios.defaults.headers.common['X-CSRF-TOKEN'] = token.content;
// } else {
//     console.error('CSRF token not found: https://laravel.com/docs/csrf#csrf-x-csrf-token');
// }
