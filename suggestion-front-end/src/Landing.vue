<template>
  <div class="landing-container">
    <nav class="navbar navbar-expand-lg navbar-light">
      <a id="home-link" class="navbar-brand" href="/">Suggestion API</a>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNavDropdown"
        aria-controls="navbarNavDropdown"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNavDropdown">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item">
            <a id="api-documentation-link" class="nav-link" :href="apiDocumentationEndpoint" target="_blank">API documentation</a>
          </li>
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="#"
              id="navbarDropdownMenuLink"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >{{`Currently using API version ${apiVersion}`}}</a>
            <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
              <a id="set-api-v1" class="dropdown-item" href="#" v-on:click="setApiVersion('1.0')">1.0</a>
              <a id="set-api-v2" class="dropdown-item" href="#" v-on:click="setApiVersion('2.0')">2.0</a>
            </div>
          </li>
          <li class="nav-item">
            <button id="button-reset" type="button" class="btn btn-dark" v-on:click="resetData()">Reset data</button>
          </li>
        </ul>
      </div>
    </nav>

    <div class="page-container row">
      <div class="search-container col-lg-4">
        <search-box @queryChanged="queryChanged" :query="query" :responseTime="responseTime"></search-box>
        <div class="additional-parameters-container">
          <h4>Additionnal parameters:</h4>
          <div class="row additional-parameters">
            <div class="col-lg-3">
              <p>Latitude :</p>
              <input type="text" id="latitude" name="latitude" v-model="latitude" />
            </div>
            <div class="col-lg-3">
              <p>Longitude :</p>
              <input type="text" id="longitude" name="longitude" v-model="longitude" />
            </div>
            <div class="col-lg-3">
              <p>Max results :</p>
              <input type="text" id="max-results" name="max-results" v-model="maxResults" />
            </div>
            <div class="col-lg-3 col-refresh">
              <button type="button" id="button-refresh" class="btn btn-dark button-refresh" v-on:click="refreshData()">Refresh data</button>
            </div>
          </div>
        </div>

        <div class="predifined-queries-container">
          <h4>Predefined queries:</h4>
          <div class="row predifined-queries">
            <div class="col">
              <button
                id="predifined-query-1"
                type="button"
                class="btn btn-dark"
                v-on:click="predifinedQuery1()"
              >/suggestions?q=londo</button>
            </div>
            <div class="col">
              <button
                id="predifined-query-2"
                type="button"
                class="btn btn-dark"
                v-on:click="predifinedQuery2()"
              >/suggestions?q=Londo&latitude=43.70011&longitude=-79.4163</button>
            </div>
            <div class="col">
              <button
                id="predifined-query-3"
                type="button"
                class="btn btn-dark"
                v-on:click="predifinedQuery3()"
              >/suggestions?q=New&latitude=40.71427&longitude=-74.00597&n=3</button>
            </div>
          </div>
        </div>
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
      results: [],
      apiVersion: "2.0",
      query: "",
      latitude: "",
      longitude: "",
      maxResults: "10",
      responseTime: 0
    };
  },
  computed: {
    apiDocumentationEndpoint(){
      return `${process.env.VUE_APP_API_ENDPOINT}/swagger/index.html?urls.primaryName=Suggestion API - Version ${this.apiVersion}`
    }
  },
  methods: {
    queryChanged(query) {
      this.results = [];
      this.responseTime = 0;
      if (query.length >= queryMinimumLength) {
        this.query = query;
        this.fetchData();
      }
    },
    fetchData() {
      let self = this;

      //Tracking performance
      var start = Date.now();
      self.$axios
        .get("/suggestions", {
          params: {
            q: self.query,
            latitude: self.latitude,
            longitude: self.longitude,
            n: self.maxResults
          },
          headers: { "api-version": self.apiVersion }
        })
        .then(response => {
          self.responseTime = Date.now() - start;
          self.results = response.data;
        })
        .catch(errors => {
          console.log(errors);
        });
    },
    refreshData(){
      if(this.query != "")
        this.fetchData()
    },
    resetData() {
      let self = this;
      self.$axios
        .post("/seed", {
          headers: { "api-version": self.apiVersion }
        })
        .then(() => {
          self.$toasted.success("Data was reset!", {
            theme: "bubble",
            position: "bottom-right",
            duration: 3000
          });
        })
        .catch(errors => {
          console.log(errors);
        });
    },
    setApiVersion(val) {
      this.apiVersion = val;
      this.results = [];
      this.query = "";

      this.$toasted.success(`API version was changed to ${this.apiVersion} !`, {
        theme: "bubble",
        position: "bottom-right",
        duration: 3000
      });
    },
    predifinedQuery1() {
      this.query = "londo";
      this.latitude = "";
      this.longitude = "";
      this.maxResults = "10";
    },
    predifinedQuery2() {
      this.query = "Londo";
      this.latitude = "43.70011";
      this.longitude = "-79.4163";
      this.maxResults = "10";
    },
    predifinedQuery3() {
      this.query = "New";
      this.latitude = "40.71427";
      this.longitude = "-74.00597";
      this.maxResults = "3";
    }
  },

  mounted(){
     let self = this;
      self.$axios
        .get("/status", {
          headers: { "api-version": self.apiVersion }
        })
        .then(response => {
          console.log(`API status: ${response.data.status} !`)
        })
        .catch(errors => {
          console.log(errors);
        });
  }
};
</script>