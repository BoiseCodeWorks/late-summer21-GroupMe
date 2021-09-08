import { AppState } from '../AppState'
import { api } from './AxiosService'

class GroupsService {
  async getMyGroups() {
    const res = await api.get('account/groups')
    AppState.myGroups = res.data
  }

  async getAllGroups() {
    const res = await api.get('api/groups')
    AppState.groups = res.data
  }

  async getById(id) {
    const res = await api.get('api/groups/' + id)
    AppState.activeGroup = res.data
  }

  async getGroupMembers(id) {
    const res = await api.get(`api/groups/${id}/members`)
    AppState.activeGroupMembers = res.data
  }

  async leave() {
    const gm = AppState.activeGroupMembers.find(m => AppState.account.id === m.id)
    if (!gm) {
      throw new Error('You Are Not in this Group')
    }
    await api.delete('api/groupmembers/' + gm.groupMemberId)
    AppState.activeGroupMembers.filter(m => m.id === AppState.account.id)
  }

  async join() {
    const gm = AppState.activeGroupMembers.find(m => AppState.account.id === m.id)
    if (gm) {
      throw new Error('You Are Already in this Group')
    }
    const res = await api.post('api/groupmembers', { groupId: AppState.activeGroup.id })
    this.getGroupMembers(res.data.groupId)
  }
}

export const groupsService = new GroupsService()
