import Vue from 'vue'
import Router from 'vue-router'


//Components
Vue.use(Router)

let router = new Router({
    linkExactActiveClass: 'active',
    mode: 'history',
    routes: [
      { path: '*', redirect: '/' },  
      { path: '/',
        name : 'landing',
        component: () => import(/* webpackChunkName: "404" */ './Landing.vue'),
      },
    ]
  })

  export default router