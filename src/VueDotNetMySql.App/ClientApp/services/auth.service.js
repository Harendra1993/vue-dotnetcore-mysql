import axios from "axios";

const API_URL = "/api/account/";

class AuthService {
  login(user) {
    return axios
      .post(API_URL + "login", {
        UserName: user.username,
        Password: user.password
      })
      .then(({
        data
      }) => {
        if (data.statusCode == 200) {
          return data.result;
        } else {
          throw new Error(data.result.errorMessage, data)
        }

      });
  }

  logout() {
    localStorage.removeItem("user");
  }

  register(user) {
    return axios.post(API_URL + "signup", {
      username: user.username,
      email: user.email,
      password: user.password
    });
  }
}

export default new AuthService();
