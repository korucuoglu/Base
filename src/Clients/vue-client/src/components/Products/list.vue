<template>
  <div class="container mt-5">
    <h1 class="text-center">Ürün Listesi</h1>
    <br />
    <div class="row">
      <ProductItem v-for="item in items" :key="item" :item="item" />
    </div>
  </div>
</template>

<script>
import ProductItem from '@/components/Products/item'
export default {
  name: 'Home',
  components: {
    ProductItem,
  },

  props: ['items'],
}
</script>
