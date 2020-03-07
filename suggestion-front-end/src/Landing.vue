<template>
  <div class="landing-container">
    <nav class="navbar navbar-expand-lg navbar-light">
      <a class="navbar-brand" href="/">Suggestion API</a>
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
              <a class="dropdown-item" href="#" v-on:click="setApiVersion('1.0')">1.0</a>
              <a class="dropdown-item" href="#" v-on:click="setApiVersion('2.0')">2.0</a>
            </div>
          </li>
          <li class="nav-item">
            <button type="button" class="btn btn-dark" v-on:click="resetData()">Reset data</button>
          </li>
        </ul>
      </div>
    </nav>

    <div class="page-container row">
      <div class="search-container col-lg-4">
        <search-box @queryChanged="queryChanged" :query="query"></search-box>
        <div class="additional-parameters-container">
          <h4>Additionnal parameters:</h4>
          <div class="row additional-parameters">
            <div class="col-lg-4">
              <p>Latitude :</p>
              <input type="text" id="latitude" name="latitude" v-model="latitude" />
            </div>
            <div class="col-lg-4">
              <p>Longitude :</p>
              <input type="text" id="longitude" name="longitude" v-model="longitude" />
            </div>
            <div class="col-lg-4">
              <p>Max results :</p>
              <input type="text" id="max-results" name="max-results" v-model="maxResults" />
            </div>
          </div>
        </div>

        <div class="predifined-queries-container">
          <h4>Predefined queries:</h4>
          <div class="row predifined-queries">
            <div class="col">
              <button
                type="button"
                class="btn btn-dark"
                v-on:click="predifinedQuery1()"
              >/suggestions?q=londo</button>
            </div>
            <div class="col">
              <button
                type="button"
                class="btn btn-dark"
                v-on:click="predifinedQuery2()"
              >/suggestions?q=Londo&latitude=43.70011&longitude=-79.4163</button>
            </div>
            <div class="col">
              <button
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
      maxResults: "10"
    };
  },
  watch: {
    latitude: function() {
      this.fetchData();
    },
    longitude: function() {
      this.fetchData();
    },
    maxResults: function() {
      this.fetchData();
    }
  },
  methods: {
    queryChanged(query) {
      this.results = [];
      if (query.length >= queryMinimumLength) {
        this.query = query;
        this.fetchData();
      }
    },
    fetchData() {
      let self = this;
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
          self.results = response.data;
        })
        .catch(errors => {
          console.log(errors);
        });
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
            duration : 3000
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
            duration : 3000
          });
    },
    predifinedQuery1() {
      this.query = "londo";
      this.maxResults = "10";
    },
    predifinedQuery2() {
      this.query = "Londo";
      this.latitude = "42.98339";
      this.longitude = "-81.23304";
      this.maxResults = "10";
    },
    predifinedQuery3() {
      this.query = "New";
      this.latitude = "40.71427";
      this.longitude = "-74.00597";
      this.maxResults = "3";
    }
  }
};
</script>