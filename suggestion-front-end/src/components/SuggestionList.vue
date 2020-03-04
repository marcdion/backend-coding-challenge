<template>
    <ul>
        <suggestion-list-item v-for="(suggestion, index) in results" :key="index"></suggestion-list-item>
    </ul>
</template>

<script>
    import SuggestionListItem from './SuggestionListItem.vue'

    export default {
    name: 'landing',
        components: {
            SuggestionListItem
        },
        props: {
            results: {
                type: Array,
                default(){
                    return [];
                }
            }
        }
    }
</script>
