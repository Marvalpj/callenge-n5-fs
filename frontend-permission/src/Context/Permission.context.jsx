import React from 'react'
import API from '../api'

const PermissionContext = React.createContext();

const PermissionProvider = ({children}) => {
  const [permissions, setPermissions] = React.useState([])
  const [permissionsType, setPermissionsType] = React.useState([])

  React.useEffect(() => {
    (async() => {
      const resPermissionsType = await API.get('permissiontype')
      console.log(resPermissionsType)
    })()
  }, [])
  
  React.useEffect(() => {
    (async() => {
      const resPermissions = await API.get('permission')
      console.log(resPermissions)
    })()
  }, [])

  const createPermission = async () => {

  }
  
  const createPermissionType = async () => {

  }

  const permissionsUtils = {
    permissions,
    permissionsType,
    createPermission,
    createPermissionType
  }

  return (
    <PermissionContext.Provider value={permissionsUtils}>
      {children}
    </PermissionContext.Provider>
  )

}

const usePermission = () => {
  const permissions = React.useContext(PermissionContext)
  return permissions
}

export {
  PermissionProvider,
  usePermission
}