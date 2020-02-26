let actions = {
  ADD_TODO({ commit }, todo) {
    axios
      .post("/api/todos", todo)
      .then(res => {
        if (res.data === "added") console.log("ok");
      })
      .catch(err => {
        console.log(err);
      });
  },
  DELETE_TODO({ commit }, todo) {
    axios
      .delete(`/api/todos/${todo.id}`)
      .then(res => {
        if (res.data === "deleted") console.log("deleted");
      })
      .catch(err => {
        console.log(err);
      });
  },
  GET_TODOS({ commit }) {
    axios
      .get("/api/todos")
      .then(res => {
        {
          console.log(res.data);
          commit("GET_TODOS", res.data);
        }
      })
      .catch(err => {
        console.log(err);
      });
  },
  GET_FULL_COUNTS({ commit }) {
    axios
      .get("/api/count/all")
      .then(res => {
        {
          console.log(res.data);
          commit("GET_FULL_COUNTS", res.data);
        }
      })
      .catch(err => {
        console.log(err);
      });
  },
  GET_SEARCH_COUNTS({ commit }) {
    axios
      .get("/api/count/all")
      .then(res => {
        {
          console.log(res.data);
          commit("GET_SEARCH_COUNTS", res.data);
        }
      })
      .catch(err => {
        console.log(err);
      });
  }
};
export default actions;
