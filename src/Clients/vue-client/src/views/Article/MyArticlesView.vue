<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-3">
        <CategoryList @selected-category="selectedCategory = $event" />
      </div>
      <div class="col-9">
        <h1 class="text-center">My Articles</h1>
        <br />
        <p>
          <router-link :to="{ name: 'AddArticleView' }" class="btn btn-success">
            <i class="fas fa-plus-circle"></i> Add New Article
          </router-link>
        </p>

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
    this.$store.dispatch('categories/fetchList')
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
