<template>
  <div class="landing-container">
    <div class="page-title">
      <h3>Suggestion API</h3>
    </div>

    <div class="page-container">
      <div class="search-container">
        <search-box></search-box>
      </div>
      <div class="results-container">
        <suggestion-list></suggestion-list>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBox from "./components/SearchBox.vue";
import SuggestionList from "./components/SuggestionList.vue";

export default {
  name: "landing",
  components: {
    SearchBox,
    SuggestionList
  }
};
</script>