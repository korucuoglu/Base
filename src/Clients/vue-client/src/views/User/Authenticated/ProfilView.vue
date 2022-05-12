<template>
  <div class="container">
    <div class="row mt-5">
      <div class="col-4 offset-4">
        <div class="card">
          <div class="card-header text-center">Hesap Detayları</div>
          <div class="card-body">
            <div class="mb-3">
              <label class="form-label">Kullanıcı Adı</label>
              <input
                type="text"
                class="form-control"
                v-model="userData.username"
              />
            </div>
            <div class="mb-3">
              <label class="form-label">E-posta adresi</label>
              <input
                type="email"
                class="form-control"
                v-model="userData.email"
              />
            </div>
            <div class="mb-3">
              <label class="form-label">Şifreniz</label>
              <input
                type="password"
                class="form-control"
                v-model="userData.password"
              />
            </div>
            <div class="mb-3 d-flex justify-content-end align-items-center">
              <button
                class="btn btn-sm btn-primary"
                @click.prevent="update"
                :disabled="!activeButton"
              >
                Güncelle
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  data() {
    return {
      userData: null,
    }
  },
  methods: {
    async update() {
      this.$store.dispatch('users/update', this.userData)
    },
  },

  computed: {
    ...mapGetters({
      currentUser: 'users/currentUser',
    }),

    activeButton() {
      if (this.userData.password) {
        if (
          this.userData.email !== this.currentUser.email ||
          this.userData.username !== this.currentUser.username
        ) {
          return true
        }
      }
      return false
    },
  },

  created() {
    this.userData = this.copy(this.currentUser)
  },
}
</script>
