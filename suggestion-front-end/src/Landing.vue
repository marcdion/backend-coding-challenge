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
            <a id="api-documentation-link" class="nav-link" :href="apiDocumentationEndpoint" target="_blank">{{$t('apiDocumentation')}}</a>
          </li>
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="#"
              id="navbarDropdownLanguageLink"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >{{`${$t(currentLanguage)}`}}</a>
            <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
              <a id="set-language-fr" class="dropdown-item" href="#" v-on:click="setLocale('fr')">{{$t('frLong')}}</a>
              <a id="set-language-en" class="dropdown-item" href="#" v-on:click="setLocale('en')">{{$t('enLong')}}</a>
            </div>
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
            >{{`${$t('apiVersion')} ${apiVersion}`}}</a>
            <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
              <a id="set-api-v1" class="dropdown-item" href="#" v-on:click="setApiVersion('1.0')">1.0</a>
              <a id="set-api-v2" class="dropdown-item" href="#" v-on:click="setApiVersion('2.0')">2.0</a>
            </div>
          </li>
           <li class="nav-item">
              <a id="raw-api" class="btn btn-dark raw-api-button" :href="rawApiEndpoint" target="_blank">{{$t('rawApi')}}</a>
          </li>
          <li class="nav-item">
            <button id="button-reset" type="button" class="btn btn-dark" v-on:click="resetData()">{{$t('resetData')}}</button>
          </li>
        </ul>
      </div>
    </nav>

    <div class="page-container row">
      <div class="search-container col-sm-12 col-xl-4">

        <search-box @queryChanged="queryChanged" :query="query" :responseTime="responseTime"></search-box>
        
        <div class="additional-parameters-container">
          <h4>{{$t('additionalParameters')}}</h4>
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
              <p>{{$t('maxResults')}}</p>
              <input type="text" id="max-results" name="max-results" v-model="maxResults" @keypress="isNumber($event)" />
            </div>
            <div class="col-lg-3 col-refresh">
              <button type="button" id="button-refresh" class="btn btn-dark button-refresh" v-on:click="refreshData()">{{$t('refreshData')}}</button>
            </div>
          </div>
        </div>

        <predefined-queries @predefinedQuery1="predefinedQuery1()"
                            @predefinedQuery2="predefinedQuery2()"
                            @predefinedQuery3="predefinedQuery3()">
        </predefined-queries>

      </div>
      <div class="results-container col-sm-12 col-xl-8">
        <suggestion-table :results="results" :api-version="apiVersion"></suggestion-table>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBox from "./components/SearchBox.vue";
import SuggestionTable from "./components/SuggestionTable.vue";
import PredefinedQueries from "./components/PredefinedQueries.vue";

import i18n from '@/plugins/i18n';

const queryMinimumLength = 3;

export default {
  name: "landing",
  components: {
    SearchBox,
    SuggestionTable,
    PredefinedQueries
  },
  data() {
    return {
      results: [],
      apiVersion: "2.0",
      query: "",
      latitude: "",
      longitude: "",
      maxResults: "10",
      responseTime: 0,
      currentLanguage: 'fr'
    };
  },
  computed: {
    apiDocumentationEndpoint(){
      return `${process.env.VUE_APP_API_ENDPOINT}/swagger/index.html?urls.primaryName=Suggestion API - Version ${this.apiVersion}`
    },
    rawApiEndpoint(){
      return `${process.env.VUE_APP_API_ENDPOINT}/suggestions?q=Quebec`
    },
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
          if(response.status == 200)
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
          self.$toasted.success(self.$t('dataWasReset'), {
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

      this.$toasted.success(`${this.$t('apiVersionChanged')} ${this.apiVersion} !`, {
        theme: "bubble",
        position: "bottom-right",
        duration: 3000
      });
    },
    predefinedQuery1() {
      this.query = "londo";
      this.latitude = "";
      this.longitude = "";
      this.maxResults = "10";
    },
    predefinedQuery2() {
      this.query = "Londo";
      this.latitude = "43.70011";
      this.longitude = "-79.4163";
      this.maxResults = "10";
    },
    predefinedQuery3() {
      this.query = "New";
      this.latitude = "40.71427";
      this.longitude = "-74.00597";
      this.maxResults = "3";
    },
    isNumber(evt) {
      evt = (evt) ? evt : window.event;
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
          return false;
      }
      return true;
    },
    setLocale(locale){
        i18n.locale = locale;
        this.currentLanguage = locale;

        this.$toasted.success(`${this.$t('languageChangeFor')} ${this.$t(locale)} !`, {
          theme: "bubble",
          position: "bottom-right",
          duration: 3000
        });
    }
  },

  mounted(){
    //Validate that API is up and running
     let self = this;
      self.$axios
        .get("/status", {
          headers: { "api-version": self.apiVersion }
        })
        .then(response => {
          console.log(`API status: ${response.data.message} !`)
        })
        .catch(errors => {
          console.log(errors);
        });
  }
};
</script>