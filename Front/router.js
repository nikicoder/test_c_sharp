import Vue from 'vue'
import Router from 'vue-router'
import Home from './components/HomeComponent.vue'
import TaskShow from './components/TaskShowComponent.vue'

Vue.use(Router)

let appRouter = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/task/show/:id',
      name: 'task-show',
      component: TaskShow
    }
  ]
})

export default appRouter
