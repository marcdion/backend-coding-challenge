<template>
    <div class="search-box-container">
        <h3>🔍 What are you looking for ?</h3>
        <div class="search-bar">
            <input type="search" id="suggestion-search" name="suggestion-search" v-model="query">
        </div>
    </div>
</template>

<script>

    export default {
    name: 'seatch-box',
        components: {
            
        },
        props: {

        },
        watch: {
            query: function (val) {
                this.$emit('queryChanged', val)
            }
        },
        data(){
            return {
                query: ""
            }
        }
    }
</script>
