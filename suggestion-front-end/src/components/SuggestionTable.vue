<template>
  <div>
    <table class="table suggestion-table">
      
      <thead>
        <tr>
          <th scope="col">#</th>
          <th>{{$t('locationName')}}</th>
          <th>{{$t('latitude')}}</th>
          <th>{{$t('longitude')}}</th>
          <th>{{$t('score')}}</th>
          <th>{{$t('details')}}</th>
        </tr>
      </thead>
      <tbody>
        <suggestion-table-item
          id="suggestion-table-item"
          v-for="(suggestion, index) in results"
          :key="index"
          :result="suggestion"
          :index="index"
        ></suggestion-table-item>
      </tbody>
    </table>

    <p class="no-results-to-show" v-if="results.length == 0">{{noResults}}</p>
  </div>
</template>

<script>
import SuggestionTableItem from "./SuggestionTableItem.vue";

export default {
  name: "suggestion-table",
  components: {
    SuggestionTableItem
  },
  props: {
    results: {
      type: Array,
      default() {
        return [];
      }
    },
    apiVersion: {
      type: String,
      default: "2.0"
    }
  },
  computed:{
    noResults(){
      if(this.apiVersion == "2.0")
        return this.$t('noResults')
      else
        return this.$t('deprecated')
    }
  }
};
</script>
