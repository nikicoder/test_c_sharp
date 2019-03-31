import Vue from 'vue'
import VueRouter from 'vue-router'
import BootstrapVue from 'bootstrap-vue'
import AsyncComputed from 'vue-async-computed'
import VueSidebarMenu from 'vue-sidebar-menu'

import router from './router'
import store from './store'

import App from './components/FrontTaskBoardApp.vue'

//import 'bootstrap-vue/dist/bootstrap-vue.css'
import './assets/style.scss'

Vue.config.productionTip = false
Vue.use(VueRouter)
Vue.use(BootstrapVue)
Vue.use(AsyncComputed)
Vue.use(VueSidebarMenu)

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')