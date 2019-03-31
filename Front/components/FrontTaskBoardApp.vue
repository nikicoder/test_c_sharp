<template>
    <div id="page">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
            <a class="navbar-brand" href="/">Tasks Board</a>
        </nav>

        <main class="container" role="main">

            <MenuComponent/>

            <div id="page-wrapper" style="min-height: 367px;">
            <router-view></router-view>
        </div>
        </main>   
    </div>
</template>

<script>

import MenuComponent from './TasksMenuComponent.vue'

export default {
    name: 'app',
    components: {
        MenuComponent
    }
 }
</script>