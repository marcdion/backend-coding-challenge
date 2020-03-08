<template>
    <tr class="suggestion-table-item">
         <th scope="row">{{index + 1}}</th>
         <th>{{result.name}}</th>
         <th>{{result.latitude}}</th>
         <th>{{result.longitude}}</th>
         <th>{{result.score}}</th>
         <a :id="gMapsId" class="suggestion-item-link" :href="addressLink" target="_blank">
            <th class="suggestion-item-th">...</th>
         </a>
    </tr>
</template>

<script>
    export default {
    name: 'suggestion-table-item',
        components: {
            
        },
        props: {
            result: {
                type: Object,
                default: null
            },
            index: {
                type: Number,
                default: 0
            }
        },
        computed: {
            addressLink(){
                if(this.result.latitude && this.result.longitude)
                    return `https://www.google.com/maps/search/?api=1&query=${this.result.latitude},${this.result.longitude}`
                else
                    return 'https://www.google.com/maps'
            },
            gMapsId(){
                return `suggestion-item-link-${this.index}`
            }
        }
    }
</script>
