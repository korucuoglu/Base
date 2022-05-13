<template>
  <ProductList :items="productList" />
</template>

<script>
import ProductList from '@/components/Products/list'
export default {
  name: 'Home',
  components: {
    ProductList,
  },
  data() {
    return {
      productList: [],
    }
  },
}
</script>
