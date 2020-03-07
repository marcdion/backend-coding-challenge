<template>
  <div class="search-box-container">
    <h3>🔍 What are you searching for ?</h3>
    <div class="search-bar">
      <input type="search" id="suggestion-search" name="suggestion-search" v-model="input" />
    </div>
  </div>
</template>

<script>
export default {
  name: "seatch-box",
  props: {
    query: {
      type: String,
      default() {
        return "";
      }
    }
  },
  watch: {
    query: function(val) {
      this.input = val;
    },
    input: function(val) {
      this.$emit("queryChanged", val);
    }
  },
  data() {
    return {
      input: ""
    };
  }
};
</script>
