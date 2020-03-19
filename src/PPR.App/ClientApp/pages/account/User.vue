<template>
  <modal v-model="open" @hide="callback" ref="modal" size="md">
    <span slot="title"><i class="far fa-user"></i> Create New User</span>
    <div class="ibox shadow-md-down">
      <div class="ibox-content">
        <form class="m-t" role="form" @submit.prevent="handleUserCreate">
          <div
            class="form-group row"
            :class="{ 'has-error': submitted && $v.user.username.$error }"
          >
            <label class="col-lg-2 col-form-label">User Name</label>

            <div class="col-lg-10">
              <input
                type="text"
                v-model.trim="$v.user.username.$model"
                placeholder="User Name"
                class="form-control"
              />
              <!-- <span class="form-text m-b-none"
                >Example block-level help text here.</span
              > -->
              <div
                v-if="submitted && !$v.user.username.required"
                class="help-block with-errors"
              >
                <b>Username is required!</b>
              </div>
            </div>
          </div>
          <div
            class="form-group row"
            :class="{ 'has-error': submitted && $v.user.roles.$error }"
          >
            <label class="col-lg-2 col-form-label">Role</label>

            <div class="col-lg-10">
              <select
                v-model.trim="$v.user.roles.$model"
                class="select2_demo_2 form-control"
                multiple="multiple"
              >
                <option value="Super Admin">Super Admin</option>
                <option value="Admin">Admin</option>
                <option value="User">User</option>
              </select>
              <div
                v-if="submitted && !$v.user.roles.required"
                class="help-block with-errors"
              >
                <b>Seect atleast one Role !</b>
              </div>
            </div>
          </div>

          <div class="form-group row">
            <div class="col-lg-offset-2 col-lg-10">
              <button class="btn btn-primary">
                <i class="far fa-check-circle"></i> Create User
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div slot="footer">
      <btn @click="open = false">Cancel</btn>
    </div>
  </modal>
</template>

<script>
import { required } from "vuelidate/lib/validators";
import { helpers } from "@/utils";

export default {
  data() {
    return {
      open: false,
      user: {
        username: "",
        roles: []
      },
      loading: false,
      submitted: false
    };
  },

  validations: {
    user: {
      username: { required },
      roles: { required }
    }
  },

  methods: {
    show: function(row) {
      const vm = this;
      if (row != undefined) {
        console.log(row);
        vm.user.username = row.userName;
      } else {
        vm.user.username = "";
        vm.user.roles = [];
      }
      vm.open = true;
    },
    handleUserCreate() {
      const vm = this;
      this.submitted = true;

      // stop here if form is invalid
      this.$v.$touch();
      if (this.$v.$invalid) {
        return;
      }

      this.loading = true;
      if (this.user.username && this.user.roles) {
        helpers.post("/api/account/createuser", this.user).then(({ data }) => {
          console.log("added");
        });
      }
    },
    handleUserUpdate() {
      this.submitted = true;

      // stop here if form is invalid
      this.$v.$touch();
      if (this.$v.$invalid) {
        return;
      }
    },
    callback(msg) {
      //this.$notify(`Modal dismissed with msg '${msg}'.`);
    }
  }
};
</script>

