<template>
    <div id="task"> 
        <h3>Task #{{ taskData.id }}</h3>

        <strong>Title:</strong> {{ taskData.taskName }} <br>
        <strong>Content:</strong> {{ taskData.taskContent }} <br>
        <strong>Employees:</strong> {{ taskData.employeesList }} <br>
        <strong>Added:</strong> {{ taskData.dateAdd }} <br>
        <strong>Plan Hours:</strong> {{ taskData.planHours }} <br>
        <strong>TotalHours:</strong> {{ taskData.totalHours }} 
    </div>
    
</template>

<script>

import axios from 'axios'

export default {
    name: 'task-show',
    asyncComputed: {
        taskData:  {
            get () {
                return axios.get('/api/task/'+ this.$route.params.id)
                .then(response => {
                    return response.data
                })
            },
            default: { 
                taskName: "",
                taskContent: "",
                employeesList: "",
                dateAdd: "",
                planHours: "",
                totalHours: ""
            }
        }
    }
 }
</script>