import React from 'react'
import API from '../api'
import { toast } from 'sonner';

const PermissionContext = React.createContext();

const PermissionProvider = ({children}) => {
  const [permissions, setPermissions] = React.useState([])
  const [permissionsType, setPermissionsType] = React.useState([])

  React.useEffect(() => {
    (async() => {
      await getPermisionsTypes()
    })()
  }, [])
  
  React.useEffect(() => {
    (async() => {
      await getPermisions()
    })()
  }, [])

  const getPermisions = async () => {
    const resPermissions = await API.get('permission')
      .catch(e => {
      })

      if(resPermissions.data)
        setPermissions(resPermissions.data)
  }
  
  const getPermisionsTypes = async () => {
    const resPermissionsType = await API.get('permissiontype')
      .catch(e => {

      })
      
      if(resPermissionsType.data)
        setPermissionsType(resPermissionsType.data)
  }

  const createPermission = async (firstName, lastName, permissionType ) => {
    const body = {
      NameEmployee: firstName,
      LastNameEmployee: lastName,
      PermissionTypeId: permissionType,
      date: new Date()
    }

    const resPermissionsType = await API.post('permission',{
      ...body
    }
    )
    .catch(e => {

    })

    toast.success('Permiso creado con exito.');
    await getPermisions()
  }
  
  const createPermissionType = async (description) => {

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