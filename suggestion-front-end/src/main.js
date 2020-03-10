import Vue from 'vue'
import App from './Landing.vue'
import router from './router'

import Axios from 'axios'
import Toasted from 'vue-toasted';
import i18n from '@/plugins/i18n';

import 'bootstrap/dist/css/bootstrap.min.css'
import '@/sass/_styles.scss';
import 'bootstrap'

require('dotenv').config()
Axios.defaults.baseURL = process.env.VUE_APP_API_ENDPOINT;
Axios.defaults.withCredentials = false;

Vue.config.productionTip = false

//Instance properties
Vue.prototype.$axios = Axios;

Vue.use(Toasted)

new Vue({
  router,
  i18n,
  render: h => h(App),
}).$mount('#app')
