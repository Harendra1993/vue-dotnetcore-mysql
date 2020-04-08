import store from "@/store";

export default function authHeader() {
  const user = (store.state.auth) ? store.state.auth.user : null;

  axios.interceptors.request.use(function (config) {
    if (user && user.token) {
      config.headers.Authorization = `Bearer ${user.token}`;
    }
    return config;
  });
}
