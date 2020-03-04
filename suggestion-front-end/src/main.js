import Vue from 'vue'
import App from './Landing.vue'

import '@/sass/_styles.scss';

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
