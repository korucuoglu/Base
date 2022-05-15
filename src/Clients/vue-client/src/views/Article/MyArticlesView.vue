<template>
  <div class="container mt-5">
    <h1 class="text-center">My Articles</h1>
    <br />
    <ArticleList :items="items" />
  </div>
</template>

<script>
import ArticleList from '@/components/Articles/list'
import ArticleService from '@/services/article'
export default {
  name: 'Home',
  components: {
    ArticleList,
  },
  data() {
    return {
      items: [],
    }
  },

  async mounted() {
    this.items = await ArticleService.getMyNotes()
  },
}
</script>
