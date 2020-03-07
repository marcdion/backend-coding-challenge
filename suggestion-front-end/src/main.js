import Vue from 'vue'
import App from './Landing.vue'
import router from './router'

import Axios from 'axios'
import Utils from './utils'
import Toasted from 'vue-toasted';


import 'bootstrap/dist/css/bootstrap.min.css'
import '@/sass/_styles.scss';
import 'bootstrap'

require('dotenv').config()
Axios.defaults.baseURL = process.env.VUE_APP_API_ENDPOINT;
Axios.defaults.withCredentials = false;

Vue.config.productionTip = false

//Instance properties
Vue.prototype.$u = Utils;
Vue.prototype.$axios = Axios;

Vue.use(Toasted)

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
