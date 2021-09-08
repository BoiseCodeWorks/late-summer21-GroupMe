<template>
  <div class="group-details container">
    <div class="d-flex p-3 mt-4 bg-light">
      <img :src="group.img" alt="group image">
      <div class="p-3 flex-grow-1">
        <div class="d-flex">
          <div>
            <h1>{{ group.name }}</h1>
            <p>{{ group.description }}</p>
          </div>
          <div>
            <button v-if="state.isMember" class="btn btn-danger" @click="leave">
              Leave
            </button>
            <button v-else class="btn btn-primary" @click="join">
              Join
            </button>
          </div>
        </div>
        <div class="border p-3">
          <h3>Members</h3>
          <GroupMember v-for="gm in state.groupMembers" :key="gm.id" :group-member="gm" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { groupsService } from '../services/GroupsService'
export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      groupMembers: computed(() => AppState.activeGroupMembers),
      isMember: computed(() => {
        const found = AppState.activeGroupMembers.find(m => {
          return m.id === AppState.account.id
        })
        console.log(found)
        // NOTE !! turns truthy/falsy to a true bool
        return !!found
      })
    })

    onMounted(async() => {
      try {
        const found = AppState.groups.find(g => g.id === route.params.id)
        if (found) {
          AppState.activeGroup = found
        } else {
          await groupsService.getById(route.params.id)
        }
        await groupsService.getGroupMembers(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })

    return {
      state,
      group: computed(() => AppState.activeGroup),
      async leave() {
        try {
          await groupsService.leave()
          Pop.toast('Left Group', 'info')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async join() {
        try {
          await groupsService.join()
          Pop.toast('Joined Group', 'info')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  width: 300px
}
</style>
