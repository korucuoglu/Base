<template>
  <div class="list-group">
    <a
      href="#"
      @click="selectedCategory = null"
      class="list-group-item list-group-item-action active"
      aria-current="true"
    >
      Tüm Kategoriler
    </a>
    <a
      v-for="category in categories"
      :key="category.id"
      href="#"
      @click="selectedCategory = category.id"
      class="list-group-item list-group-item-action"
      >{{ category.title }}</a
    >
  </div>
</template>

<script>
import CategoryService from '@/services/category'
export default {
  data() {
    return {
      categories: null,
      selectedCategory: null,
    }
  },
  async mounted() {
    this.categories = await CategoryService.getAll()
  },
  watch: {
    async selectedCategory(n) {
      this.$emit('selected-category', n)
    },
  },
}
</script>
