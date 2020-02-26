<template>
  <div id="search_container">
    <div class="search-form">
      <form role="search" @submit.prevent="change($event)">
        <div class="input-group shadow-md-down" id="search-placeholder">
          <div class="input-group-btn">
            <button tabindex="-1" class="btn btn-white open" type="button" data-link="type">{{type}}</button>

            <button
              data-toggle="dropdown"
              class="btn btn-white dropdown-toggle"
              type="button"
              aria-expanded="true"
            >
              <span class="caret"></span>
            </button>

            <ul class="dropdown-menu" id="typeSelect" @click="setType($event)">
              <li :class="(type=='Partial'?'active':'')">
                <a id="Partial">Partial</a>
              </li>

              <li :class="(type=='Exact'?'active':'')">
                <a id="Exact">Exact</a>
              </li>
            </ul>
          </div>

          <input
            type="text"
            placeholder="Enter any keyword"
            class="form-control"
            required
            autofocus
            v-model="keyword"
          />

          <div class="input-group-btn">
            <button class="btn btn-primary" type="submit">
              <span class="fa fa-search"></span>
              Search
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      keyword: this.$store.state.globals.keyword,
      type: this.$store.state.globals.searchType
    };
  },

  computed: {},

  mounted() {
    //console.log(this.$store.state.globals.keyword);
  },

  methods: {
    change(e) {
      this.$store.commit("CHANGE_KEYWORD", this.keyword);
      this.$root.$emit("onSearchBoxChange", { reload: true });
    },
    setType(e) {
      this.type = e.target.innerText;
      this.$store.commit("CHANGE_SEARCH_TYPE", this.type);
      this.$root.$emit("onSearchBoxChange", { reload: true });
    }
  }
};
</script>


