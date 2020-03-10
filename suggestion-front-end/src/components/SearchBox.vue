<template>
  <div class="search-box-container">
    <h3>🔍 {{$t('whatAreYouSearchingFor')}}</h3>
    <div class="search-bar">
      <input type="search" id="suggestion-search" name="suggestion-search" v-model="input" />
    </div>
    <p v-show="responseTime != 0" class="response-time">{{`Response time: ${responseTime} ms`}}</p>
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
    },
    responseTime: {
      type: Number,
      default() {
        return 0;
      }
    },
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
