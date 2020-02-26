<template>
  <div class="row">
    <div class="col-lg-12">
      <ul class="nav navbar-top-links">
        <li>
          <router-link to="/profile">
            <i class="fa fa-users"></i> Profile
          </router-link>
        </li>
        <li>
          <router-link to="/contacts">
            <i class="fa fa-address-book"></i> Contacts
          </router-link>
        </li>

        <li>
          <router-link to="/messengers">
            <i class="fa fa-comments"></i> Messengers
          </router-link>
        </li>

        <li>
          <router-link to="/sms">
            <i class="fa fa-sms"></i> SMS
          </router-link>
        </li>

        <li>
          <router-link to="/emails-old">
            <i class="fa fa-envelope"></i> Emails
          </router-link>
        </li>
        <!-- File System Start-->
        <li>
          <router-link to="/attachments">
            <i class="fa fa-paperclip"></i>
            Mobile-attachments
          </router-link>
        </li>

        <li>
          <router-link to="/tagged" id="files">
            <i class="fa fa-tag"></i>
            Tagged-reports
          </router-link>
        </li>

        <li class="dropdown">
          <a class="dropdown-toggle" id="untagged" data-toggle="dropdown" href="#">
            <i class="fa fa-folder"></i>
            Un-tagged reports
            <i class="caret"></i>
          </a>

          <ul class="dropdown-menu">
            <li>
              <router-link to="/untagged/files">
                <i class="fa fa-file-search"></i> Files
              </router-link>
            </li>

            <li>
              <router-link to="/untagged/ppt">
                <i class="fa fa-file-powerpoint"></i> PPT
              </router-link>
            </li>
          </ul>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>