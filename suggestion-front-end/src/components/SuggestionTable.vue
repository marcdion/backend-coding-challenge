<template>
  <div>
    <table class="table suggestion-table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th>Location name</th>
          <th>Latitude</th>
          <th>Longitude</th>
          <th>Score</th>
          <th>Details</th>
        </tr>
      </thead>
      <tbody>
        <suggestion-table-item
          v-for="(suggestion, index) in results"
          :key="index"
          :result="suggestion"
          :index="index"
        ></suggestion-table-item>
      </tbody>
    </table>

    <p class="no-results-to-show" v-if="results.length == 0">There are no results to show</p>
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
    }
  }
};
</script>
