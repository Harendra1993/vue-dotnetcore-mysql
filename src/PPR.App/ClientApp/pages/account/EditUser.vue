<template>
  <modal v-model="open" @hide="callback" ref="modal" size="md">
    <span slot="title"><i class="far fa-user"></i> Update User</span>
    <div class="ibox shadow-md-down">
      <div class="ibox-content">
        <form class="m-t" role="form" @submit.prevent="handleUserUpdate">
          <div class="form-group row">
            <label class="col-lg-2 col-form-label">User Name</label>

            <div class="col-lg-10">
              {{ user.username }}
            </div>
          </div>
          <div
            class="form-group row"
            :class="{ 'has-error': submitted && $v.user.userRoles.$error }"
          >
            <label class="col-lg-2 col-form-label">Role</label>

            <div class="col-lg-10">
              <select
                v-model.trim="$v.user.userRoles.$model"
                class="select2_demo_2 form-control"
                multiple="multiple"
              >
                <option
                  v-for="(option, index) in userRoles"
                  :value="option"
                  :key="index"
                >
                  {{ option.roleName }}</option
                >
              </select>
              <div
                v-if="submitted && !$v.user.userRoles.required"
                class="help-block with-errors"
              >
                <b>Seect atleast one Role !</b>
              </div>
            </div>
          </div>

          <div
            class="form-group row"
            :class="{ 'has-error': submitted && $v.user.userRoles.$error }"
          >
            <label class="col-lg-2 col-form-label">Status</label>

            <div class="col-lg-10">
              <fieldset>
                <div
                  class="form-check abc-radio abc-radio-success form-check-inline"
                >
                  <input
                    class="form-check-input"
                    type="radio"
                    id="radioActive"
                    value="true"
                    name="radioInline"
                    v-model="user.isActive"
                  />
                  <label class="form-check-label" for="radioActive">
                    Active
                  </label>
                </div>
                <div
                  class="form-check abc-radio abc-radio-danger form-check-inline"
                >
                  <input
                    class="form-check-input"
                    type="radio"
                    id="radioInactive"
                    value="false"
                    name="radioInline"
                    v-model="user.isActive"
                  />
                  <label class="form-check-label" for="radioInactive">
                    Inactive
                  </label>
                </div>
              </fieldset>
            </div>
          </div>

          <div class="form-group row">
            <div class="col-lg-offset-2 col-lg-10">
              <button class="btn btn-success">
                <i class="far fa-check-circle"></i> Update User
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
import "awesome-bootstrap-checkbox";

export default {
  data() {
    return {
      open: false,
      user: {
        userId: "",
        username: "",
        userRoles: [],
        isActive: false
      },
      userRoles: [],
      loading: false,
      submitted: false
    };
  },

  validations: {
    user: {
      userRoles: { required }
    }
  },

  methods: {
    show: function(row) {
      const vm = this;
      vm.doLoadRoles();
      vm.user.userId = row.userId;
      vm.user.username = row.userName;

      vm.$v.user.userRoles.$model = row.userRoles.map(x =>
        Object.assign(x.role, { userId: x.userId })
      );

      console.log(vm.$v.user.userRoles.$model);

      vm.user.isActive = row.isActive;

      vm.open = true;
    },

    doLoadRoles() {
      const vm = this;
      const table = vm.$refs.table;

      helpers.get("/api/account/allroles").then(({ data }) => {
        if (data.message == "Success") {
          vm.userRoles = data.result.map(x =>
            Object.assign(x, { userId: vm.user.userId })
          );
        }
      });
    },

    handleUserUpdate() {
      const vm = this;
      vm.submitted = true;
      vm.$v.user.userRoles.$touch();

      // stop here if form is invalid
      vm.$v.$touch();
      if (vm.$v.$invalid) {
        return;
      }

      vm.loading = true;
      if (vm.user) {
        helpers.post("/api/account/updateuser", vm.user).then(({ data }) => {
          if (data.message == "Success") vm.$emit("edited", data, vm);
        });
        vm.loading = false;
        vm.open = false;
      }
    },

    callback(msg) {
      //this.$notify(`Modal dismissed with msg '${msg}'.`);
    }
  }
};
</script>

