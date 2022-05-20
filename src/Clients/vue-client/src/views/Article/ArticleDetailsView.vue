<template>
  <div v-if="item != null" class="container mt-5">
    <component
      :is="getComponent"
      :item="item"
      @cancel="this.editMode = false"
      @update="Update($event)"
    />
    <a v-if="isOWner && !editMode" @click="editMode = !editMode"
      >click to edit</a
    >
  </div>
</template>

<script>
import ArticleService from '@/services/article'
import ArticleRead from '@/components/Articles/ArticleRead'
import ArticleEdit from '@/components/Articles/ArticleEdit'
import { mapGetters } from 'vuex'
export default {
  components: {
    ArticleRead,
    ArticleEdit,
  },
  data() {
    return {
      item: null,
      editMode: false,
    }
  },
  async mounted() {
    this.item = await ArticleService.getById(this.$route.params.id)
  },

  computed: {
    ...mapGetters({
      currentUser: 'users/currentUser',
    }),
    isOWner() {
      return this.item.username == this.currentUser?.username
    },
    getComponent() {
      return this.editMode ? 'ArticleEdit' : 'ArticleRead'
    },
  },

  methods: {
    async Update(data) {
      this.editMode = false
      await ArticleService.update(data)
    },
  },
}
</script>
