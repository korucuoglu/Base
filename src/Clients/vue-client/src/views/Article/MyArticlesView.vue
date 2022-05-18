<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-md-3">
        <CategoryList @selected-category="selectedCategory = $event" />
      </div>
      <div class="col-md-9">
        <h1 class="text-center">My Articles</h1>
        <br />
        <ArticleList :items="items" />
      </div>
    </div>
  </div>
</template>

<script>
import ArticleList from '@/components/Articles/ArticleList'
import CategoryList from '@/components/Categories/CategoryList'
import ArticleService from '@/services/article'
export default {
  name: 'Home',
  components: {
    ArticleList,
    CategoryList,
  },
  data() {
    return {
      items: [],
      selectedCategory: null,
    }
  },

  async mounted() {
    this.items = await ArticleService.getMyNotes()
  },

  watch: {
    async selectedCategory(n) {
      if (n == null) {
        this.items = await ArticleService.getMyNotes()
        return
      }
      this.items = await ArticleService.getArticlesByCategoryId(n)
    },
  },
}
</script>
