<template>
  <div class="col-md-3 mb-2">
    <div class="card">
      <div class="card-body">
        <h5 class="card-title">{{ item.title }}</h5>
        <p class="card-text">
          {{ item.content }}
        </p>
        <a href="#" class="card-link">Card link</a>
        <a href="#" class="card-link">Another link</a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['item'],
}
</script>
