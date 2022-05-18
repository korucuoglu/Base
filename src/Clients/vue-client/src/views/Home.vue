<template>
  <div class="container mt-5">
    <h1 class="text-center">Public Articles</h1>
    <br />
    <ArticleList :items="items" />
  </div>
</template>

<script>
import ArticleList from '@/components/Articles/ArticleList'
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
    this.items = await ArticleService.getPublicNotes()
  },
}
</script>
