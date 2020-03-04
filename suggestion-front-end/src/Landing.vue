<template>
  <div class="landing-container">
    <nav class="navbar navbar-expand-lg navbar-light">
      <a class="navbar-brand" href="/">Suggestion API</a>
    </nav>

    <div class="page-container row">
      <div class="search-container col-lg-4">
        <search-box @queryChanged="queryChanged"></search-box>
      </div>
      <div class="results-container col-lg-8">
        <suggestion-table :results="results"></suggestion-table>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBox from "./components/SearchBox.vue";
import SuggestionTable from "./components/SuggestionTable.vue";

const queryMinimumLength = 3;

export default {
  name: "landing",
  components: {
    SearchBox,
    SuggestionTable
  },
  data() {
    return {
      results: []
    };
  },
  methods: {
    queryChanged(query) {
      this.results = [];
      if (query.length >= queryMinimumLength) this.fetchData(query);
    },
    fetchData(query) {
      let self = this;
      self.$axios
        .get("/suggestions", { params: { q: query } })
        .then(response => {
          self.results = response.data;
        })
        .catch(errors => {
          console.log(errors);
        });
    }
  }
};
</script>