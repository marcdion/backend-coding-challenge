import Vue from 'vue'
import App from './Landing.vue'

import Axios from 'axios'
import Utils from './utils'

import 'bootstrap/dist/css/bootstrap.min.css'
import '@/sass/_styles.scss';

require('dotenv').config()
Axios.defaults.baseURL = process.env.VUE_APP_API_ENDPOINT;
Axios.defaults.withCredentials = false;

Vue.config.productionTip = false

//Instance properties
Vue.prototype.$u = Utils;
Vue.prototype.$axios = Axios;

new Vue({
  render: h => h(App),
}).$mount('#app')
