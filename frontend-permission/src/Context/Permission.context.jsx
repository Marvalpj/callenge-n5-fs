import React from 'react'
import API from '../api'
import { toast } from 'sonner';

const PermissionContext = React.createContext();

const STATES = {
  create: 'CREATE',
  update: 'UPDATE'
}

const PermissionProvider = ({ children }) => {
  const [permissions, setPermissions] = React.useState([])
  const [permissionsType, setPermissionsType] = React.useState([])

  // state component 
  const [statusApp, setStatusApp] = React.useState(STATES.create)
  const [permissionFormData, setPermissionFormData] = React.useState({
    nameEmployee: '',
    lastNameEmployee: '',
    permissionTypeId: 0,
  })

  React.useEffect(() => {
    (async () => {
      await getPermisionsTypes()
    })()
  }, [])

  React.useEffect(() => {
    (async () => {
      await getPermisions()
    })()
  }, [])

  const getPermisions = async () => {
    const resPermissions = await API.get('permission')
      .catch(e => {
      })

    if (resPermissions.data)
      setPermissions(resPermissions.data)
  }

  const getPermisionsTypes = async () => {
    const resPermissionsType = await API.get('permissiontype')
      .catch(e => {

      })

    if (resPermissionsType.data)
      setPermissionsType(resPermissionsType.data)
  }

  const createUpdatePermission = async () => {

    switch (statusApp) {

      case STATES.update:

        await updatePermission()

        break;

      default:

        await createPermission()
    }

    cleanPermissionFormData()
    await getPermisions()
  }

  const createPermission = async () => {

    const body = {
      ...permissionFormData,
      date: new Date()
    }

    const resPermissionsType = await API.post('permission', {
      ...body
    })
      .catch(e => {
        return
      })

    toast.success('Permiso creado con exito.');
  }

  const updatePermission = async () => {

    // toamr id y sacarlo
    const id = permissionFormData.id
    delete permissionFormData.id

    const body = {
      ...permissionFormData,
      date: new Date()
    }

    const resPermissionsType = await API.put('permission/'+id, {
      ...body
    })
    .catch(e => {
        return
    })

    toast.success('Permiso modificado con exito.');
  }


  const cleanPermissionFormData = () => {
    setPermissionFormData({
      nameEmployee: '',
      lastNameEmployee: '',
      permissionTypeId: 0,
    })
    setStatusApp(STATES.create)
  }

  const createPermissionType = async (description) => {
    
    const body = {
      description
    }

    const resPermissionsType = await API.post('PermissionType', {
      ...body
    })
      .catch(e => {
        return
      })

    toast.success('Permiso creado con exito.');

    await getPermisionsTypes()

  }

  const permissionsUtils = {
    permissions,
    permissionsType,
    createUpdatePermission,
    createPermissionType,
    permissionFormData,
    setPermissionFormData,
    setStatusApp,
    cleanPermissionFormData
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