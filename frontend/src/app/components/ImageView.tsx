import Image from 'next/image'
import IterationIcon from './IterationIcon'
import { ImageType } from '@/app/types/imagetype'

interface ImageViewProps {
  image: ImageType
}

const ImageView:React.FC<ImageViewProps> = ({image}) => {

  const handleClickIteration = (icon: string) => {
    alert(icon)
  }

  const { id, title, description, url, likeCount } = image;
  
  return (
    <>
      <div id={`id-image-${ id }`} className="w-full">
        <div className="flex justify-between">
          <strong>{ title }</strong>
          <small>{ likeCount } likes</small>
        </div>
        <p className="text-sm text-justify text-slate-600">{ description }</p>
      </div>
      <Image
        src={ url } 
        alt="Image Title"
        width={600}
        height={400}
      />
      <div className="w-full justify-center flex gap-4 -translate-y-11">
        <IterationIcon icon="cancel" onClick={handleClickIteration} />
        <IterationIcon icon="favourite" onClick={handleClickIteration} />
      </div>
    </>
  )
}

export default ImageView;