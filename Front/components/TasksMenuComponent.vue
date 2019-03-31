<template>
    <div id="sidebar-menu"> 
        <sidebar-menu :menu="menu_items" :width="width" />
    </div>
    
</template>

<script>

import axios from 'axios'

function reMapData(data) {
    let result = []

    data.forEach(function (el) {

        let item = {
            href: "/task/show/" + el.item.id,
            title: el.item.taskName
        }

        if(Array.isArray(el.children) && el.children.length > 0) {
            item.child = reMapData(el.children)
        }

        result.push(item)
    })

    return result
}

export default {
    name: 'menu',
    asyncComputed: {
        menu_items: {
        get () {
            return axios.get('/api/task/tree')
            .then(response => {
                return reMapData(response.data)
            })
        },
        default: []
        }
    }
 }
</script>