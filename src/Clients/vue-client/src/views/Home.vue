<template>
  <ProductList :items="productList" />
</template>

<script>
import ProductService from '@/services/product'
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

  async mounted() {
    var datas = (await ProductService.getAll()).value
    this.productList = datas
  },
}
</script>
