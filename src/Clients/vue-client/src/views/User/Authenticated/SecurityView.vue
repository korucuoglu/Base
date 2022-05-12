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
                :value="currentUser?.username"
                disabled
              />
            </div>
            <div class="mb-3">
              <label class="form-label">Mevcut Şifreniz:</label>
              <input
                type="password"
                class="form-control"
                v-model="userData.currentPassword"
              />
            </div>
            <div class="mb-3">
              <label class="form-label">Yeni Şifreniz:</label>
              <input
                type="password"
                class="form-control"
                v-model="userData.newPassword"
              />
            </div>
            <div class="mb-3">
              <label class="form-label">Tekrar Yeni Şifreniz:</label>
              <input
                type="password"
                class="form-control"
                v-model="userData.newPasswordAgain"
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
import { useToast } from 'vue-toastification'
export default {
  data() {
    return {
      userData: {
        currentPassword: '',
        newPassword: '',
        newPasswordAgain: '',
      },
    }
  },
  methods: {
    async update() {
      if (this.userData.newPassword !== this.userData.newPasswordAgain) {
        useToast().error(
          'Yeni şifreniz ile tekrar edilen şifre birbiriyle uyuşmuyor.'
        )
        return
      }

      if (this.userData.currentPassword == this.userData.newPassword) {
        useToast().error('Yeni şifreniz eski şifreniz ile aynı olamaz.')
        return
      }

      this.$store.dispatch('users/changePassword', this.userData)
    },
  },

  computed: {
    ...mapGetters({
      currentUser: 'users/currentUser',
    }),

    activeButton() {
      if (
        this.userData.currentPassword &&
        this.userData.newPassword &&
        this.userData.newPasswordAgain
      ) {
        if (this.userData.newPassword !== this.userData.newPasswordAgain) {
          return false
        }

        if (this.userData.currentPassword == this.userData.newPassword) {
          return false
        }

        return true
      }

      return false
    },
  },
}
</script>
