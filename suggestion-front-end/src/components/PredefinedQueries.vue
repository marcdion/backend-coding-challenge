<template>
  <div class="predifined-queries-container">
        <h4>{{$t('predefinedQueries')}}</h4>
        <div class="row predifined-queries">
            <div class="col">
                <button
                id="predifined-query-1"
                type="button"
                class="btn btn-dark"
                v-on:click="predefinedQuery1()"
                >/suggestions?q=londo</button>
            </div>
            <div class="col">
                <button
                id="predifined-query-2"
                type="button"
                class="btn btn-dark"
                v-on:click="predefinedQuery2()"
                >/suggestions?q=Londo&latitude=43.70011&longitude=-79.4163</button>
            </div>
            <div class="col">
                <button
                id="predifined-query-3"
                type="button"
                class="btn btn-dark"
                v-on:click="predefinedQuery3()"
                >/suggestions?q=New&latitude=40.71427&longitude=-74.00597&n=3</button>
            </div>
        </div>
    </div>
</template>

<script>

export default {
  name: "predefined-queries",
  methods: {
    predefinedQuery1(){
        this.$emit("predefinedQuery1")
    },
    predefinedQuery2(){
        this.$emit("predefinedQuery2")
    },
    predefinedQuery3(){
        this.$emit("predefinedQuery3")
    }
  },

  mounted(){
      console.log(this.$t)
  }
};
</script>
