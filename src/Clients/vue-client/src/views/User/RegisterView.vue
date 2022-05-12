<template>
  <div>
    <div class="container loginPage">
      <div class="row">
        <div class="col-4 offset-4">
          <div class="card">
            <div class="card-header text-center">Kayıt Ol</div>
            <div class="card-body">
              <div class="mb-3">
                <label class="form-label">Kullanıcı Adı:</label>
                <input
                  type="text"
                  class="form-control"
                  placeholder="username"
                  v-model="userData.username"
                />
              </div>
              <div class="mb-3">
                <label class="form-label">E-posta adresi</label>
                <input
                  type="email"
                  class="form-control"
                  placeholder="info@Base.com"
                  v-model="userData.email"
                />
              </div>
              <div class="mb-3">
                <label class="form-label">Şifre:</label>
                <input
                  type="password"
                  class="form-control"
                  placeholder="*******"
                  v-model="userData.password"
                />
              </div>
              <div class="mb-3 d-flex justify-content-end align-items-center">
                <button class="btn btn-sm btn-primary" @click="onSubmit">
                  Kayıt Ol
                </button>
              </div>
            </div>
            <div class="card-footer text-center">
              <router-link
                :to="{ name: 'LoginView' }"
                class="p-0 m-0 btn btn-link text-primary"
                >Zaten üyeyim
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      userData: {
        username: '',
        email: '',
        password: '',
      },
    }
  },
  methods: {
    onSubmit() {
      this.$store.dispatch('users/register', this.userData)
    },
  },
}
</script>
