<template>
  <div class="group bg-light shadow rounded action">
    <router-link :to="{name: 'GroupDetailsPage', params: {id: group.id}}">
      <img class="w-100 rounded" :src="group.img" alt="group cover image">
      <div class="text-center">
        <p class="f-16">
          {{ group.name }}
        </p>
      </div>
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    group: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>
a {
  text-decoration: none;
  color: inherit;
}
</style>
