<template>
  <div class="group-member d-flex align-items-center">
    <img class="rounded-pill" :src="groupMember.picture" :alt="groupMember.name">
    <p class="m-0 ml-4">
      {{ groupMember.name }}
    </p>
  </div>
</template>

<script>
export default {
  props: {
    groupMember: {
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
img{
width: 3em
}
</style>
