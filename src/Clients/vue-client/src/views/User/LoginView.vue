<template>
  <div class="container loginPage">
    <div class="row">
      <div class="col-4 offset-4">
        <div class="card">
          <div class="card-header text-center">Giriş Yap</div>
          <div class="card-body">
            <div class="mb-3">
              <label class="form-label">Kullanıcı Adı</label>
              <input
                type="email"
                class="form-control"
                placeholder="Kullanıcı Adınızı Girin."
                v-model="userData.username"
              />
            </div>
            <div class="mb-3">
              <label class="form-label">Şifre</label>
              <input
                type="password"
                class="form-control"
                v-model="userData.password"
                placeholder="*****"
              />
            </div>
            <div class="mb-3 d-flex justify-content-end align-items-center">
              <button @click="onSubmit" class="btn btn-sm btn-primary">
                Giriş Yap
              </button>
            </div>
          </div>
          <div class="card-footer text-center">
            <router-link
              :to="{ name: 'RegisterView' }"
              class="p-0 m-0 btn btn-link text-primary"
              >Üye değilim</router-link
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserService from '@/services/user'
export default {
  data: () => ({
    userData: { username: '', password: '' },
  }),

  methods: {
    onSubmit() {
      UserService.login(this.userData)
    },
  },
}
</script>
