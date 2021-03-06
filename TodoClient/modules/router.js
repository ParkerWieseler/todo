import Vue from 'vue';
import VueRouter from 'vue-router';
import axios from 'axios';

Vue.use(VueRouter);

// Views
import Home from '.././vue/views/Home.vue';
import TodoListView from '.././vue/views/TodoListView.vue';
import Login from '.././vue/views/Login.vue';

const router = new VueRouter({
  routes: [
    { path: '/login', component: Login, name: 'Login' },
    { path: '/', component: Home, name: 'Home' },
    { path: '/lists', component: Home, name: 'My Lists' },
    { path: '/lists/:todoListId', component: TodoListView, props: true },
  ],
});

export default router;